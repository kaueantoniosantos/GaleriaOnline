import './Galeria.css'
import icon from "../../assets/img/upload.svg"
import { Botao } from '../../components/botao/Botao'
import { Card } from '../../components/card/Card'
import { useEffect, useState } from 'react'
import api from '../../Services/services'
import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

export const Galeria = () => {

    const [cards, setCards] = useState([]);
    const [imagem, setImagem] = useState(null);
    const [nomeImagem, setNomeImagem] = useState("");

    async function listarCards() {
        try {
            const resposta = await api.get("Imagem")
            setCards(resposta.data);
        } catch (error) {
            console.error("Erro ao listar:", error)
        }
    }

    async function cadastrarCard(e) {
        e.preventDefault();
        if (imagem && nomeImagem) {
            try {
                const formData = new FormData();
                formData.append("Nome", nomeImagem);
                formData.append("Arquivo", imagem);

                await api.post("Imagem/upload", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                });

                //isso aqui serve para atualizar logo assim que fizer a ação
                listarCards();

            } catch (error) {
                alert("Não foi possível realizar o cadastro.");
                console.error(error);
            }
        } else {
            alert("Preencha os campos de Nome e Imagem.");
        }
    }

    function editarCard(id, nomeAntigo) {
        const novoNome = prompt("Digite o novo nome da imagem:", nomeAntigo);

        const inputArquivo = document.createElement("input");
        inputArquivo.type = "file";
        inputArquivo.accept = "image/*";
        inputArquivo.style = "display: none";

        inputArquivo.onchange = async (e) => {
            const novoArquivo = e.target.files[0];
            const formData = new FormData();

            formData.append("Nome", novoNome);
            formData.append("Arquivo", novoArquivo);

            if (formData) {
                try {
                    await api.put(`Imagem/${id}`, formData, {
                        headers: {
                            "Content-Type": "multipart/form-data"
                        }
                    })

                    listarCards();
                } catch (error) {
                    alert("Não foi possível alterar o card!");
                    console.error(error);
                }
            }
        };

        inputArquivo.click()
    }



    async function excluirCard(id) {
        try {
            await api.delete(`Imagem/${id}`)
            listarCards();
        } catch (error) {
            alert("Erro ao excluir card.")
            console.error(error);
        }
    }

    useEffect(() => {
        listarCards();
    }, []);

    return (
        <>

            <h1 className='tituloGaleria'>Galeria Online</h1>
            <form className='formulario' onSubmit={cadastrarCard}>
                <div className='campoNome'>
                    <label>Nome</label>
                    <input
                        onChange={(e) => setNomeImagem(e.target.value)}
                        value={nomeImagem}
                        type='text'
                        className='inputNome'
                    />
                </div>
                <div className='campoImagem'>
                    <label className='arquivoLabel'>
                        <i><img src={icon} alt="Icone de upload de imagem" /></i>
                        <input
                            type="file"
                            className='arquivoInput'
                            onChange={(e) => setImagem(e.target.files[0])}
                        />
                    </label>
                </div>
                <Botao nomeBotao="Cadastrar" tipoBotao="submit" />
            </form>

            <div className='campoCards'>
                {cards.length > 0 ? (
                    cards.map((e) => (
                        <Card
                            funcaoEditar={() => editarCard(e.id, e.nome)}
                            funcaoExcluir={() => excluirCard(e.id)}
                            key={e.id}
                            tituloCard={e.nome}
                            imgCard={`https://localhost:7180/${e.caminho.replace("wwwroot/", "")}`}
                        />
                    ))
                ) : <p>Nenhum card cadastrado.</p>}
            </div>
        </>
    )
}