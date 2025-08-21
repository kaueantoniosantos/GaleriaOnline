using GaleriaOnline.WebApi.DTO;
using GaleriaOnline.WebApi.Models;
using GaleriaOnline.WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaleriaOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        private readonly ImagemRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ImagemController(ImagemRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagemPorID(int id)
        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound();
            }

            return Ok(imagem);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasAsImagens()
        {
            var imagens = await _repository.GetAllAsync();
            return Ok(imagens);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadImagem([FromForm] ImagemDto dto)
        {
            if (dto.Arquivo == null || dto.Arquivo.Length == 0 || String.IsNullOrWhiteSpace(dto.Nome))
            {
                return BadRequest("Deve ser enviado um Nome e uma Imagem");
            }

            var extensao = Path.GetExtension(dto.Arquivo.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";

            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if(!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }
            
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
            await dto.Arquivo.CopyToAsync(stream);
            }

            var imagem = new Imagem
            {
                Nome = dto.Nome,
                Caminho = Path.Combine(pastaRelativa, nomeArquivo).Replace("\\", "/"),
            };

            await _repository.CreateAsync(imagem);

            return CreatedAtAction(nameof(GetImagemPorID), new {id = imagem.Id}, imagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarImagem(int id, PutImagemDto imagemAtualizada)
        {
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                return NotFound("Imagem não encontrada.");
            }

            if(imagemAtualizada.Arquivo == null && string.IsNullOrWhiteSpace(imagemAtualizada.Nome))
                {
                return BadRequest("Pelo menos um dos campos tem que ser preenchido.");
            }

            if(!string.IsNullOrWhiteSpace(imagemAtualizada.Nome))
            {
                imagem.Nome = imagemAtualizada.Nome;
            }

            var caminhoAntigo = Path.Combine(Directory.GetCurrentDirectory(), imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if(imagemAtualizada.Arquivo != null && imagemAtualizada.Arquivo.Length > 0)
            {
                if (System.IO.File.Exists(caminhoAntigo))
                {
                    System.IO.File.Delete(caminhoAntigo);
                }

                var extensao = Path.GetExtension(imagemAtualizada.Arquivo.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";

                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await imagemAtualizada.Arquivo.CopyToAsync(stream);
                }

                imagem.Caminho = Path.Combine(pastaRelativa, nomeArquivo).Replace("\\", "/");
            }
            var atualizado = await _repository.UpdateAsync(imagem);
            if(!atualizado)
            {
                return StatusCode(500, "Erro ao atualizar a imagem");
            }
            return Ok(imagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarImagem(int id)
        {
            // 1. Primeiro, encontre a imagem no banco de dados.
            var imagem = await _repository.GetByIdAsync(id);
            if (imagem == null)
            {
                // Se a imagem não for encontrada, retorne NotFound.
                return NotFound("Imagem não encontrada.");
            }

            // 2. Tente apagar o arquivo físico no disco.
            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), imagem.Caminho.Replace("/", Path.DirectorySeparatorChar.ToString()));

            // Verifique se o arquivo existe antes de tentar apagar.
            if (System.IO.File.Exists(caminhoFisico))
            {
                try
                {
                    System.IO.File.Delete(caminhoFisico);
                }
                catch (Exception ex)
                {
                    // Se der erro ao apagar o arquivo, você pode registrar o erro,
                    // mas ainda assim tentar apagar o registro do banco.
                    Console.WriteLine($"Erro ao excluir o arquivo físico: {ex.Message}");
                }
            }

            // 3. Em seguida, apague o registro do banco de dados.
            // Esta é a parte mais importante.
            var deletado = await _repository.DeleteAsync(id);
            if (!deletado)
            {
                // Se der erro ao apagar do banco, retorne um erro 500.
                return StatusCode(500, "Erro ao excluir a imagem do banco de dados.");
            }

            // 4. Se tudo deu certo, retorne o status 204 (NoContent).
            return NoContent();
        }
    }
}
