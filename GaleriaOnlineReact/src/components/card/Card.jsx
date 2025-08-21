import './Card.css'
import imgCard from '../../assets/img/ralphzinho.jpg'
import imgPen from '../../assets/img/pen.svg'
import imgTrash from '../../assets/img/trash.svg'

export const Card = ({tituloCard}) => {
    return (
        <>
            <div className='cardDaImagem'>
                <p>{tituloCard}</p>
                <img src={imgCard} alt="" className='imgDoCard' />
                <div className='icons'>
                    <img src={imgPen} alt="Icone de caneta relacionada a editar o nome da imagem" />
                    <img src={imgTrash} alt="Icone de lixeira relacionada a exluir a imagem" />
                </div>
            </div>
        </>
    )
}