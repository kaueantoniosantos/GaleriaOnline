import './Card.css'
import imgPen from '../../assets/img/pen.svg'
import imgTrash from '../../assets/img/trash.svg'

export const Card = ({tituloCard, imgCard, funcaoEditar, funcaoExcluir}) => {
    return (
        <>
            <div className='cardDaImagem'>
                <p>{tituloCard}</p>
                <img src={imgCard} alt="" className='imgDoCard' />
                <div className='icons'>
                    <img onClick={funcaoEditar} src={imgPen} alt="Icone de caneta relacionada a editar o nome da imagem" />
                    <img onClick={funcaoExcluir} src={imgTrash} alt="Icone de lixeira relacionada a exluir a imagem" />
                </div>
            </div>
        </>
    )
}