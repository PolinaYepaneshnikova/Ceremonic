import { useEffect, useRef, useState } from "react"
import style from './css/uploadImageUser.module.css'
import { weddingAvatar } from "../http/weddingAPI";


interface UploadImageUserProps {
    avatarFileName?: string;
    zIndex?: number;
    marginLeft?: string;
    position?: 'static' | 'relative' | 'absolute' | 'fixed' | 'sticky';
    width?: string;
    height?: string;
    you?: boolean;
}


export const UploadImageUser: React.FC<UploadImageUserProps> = ({avatarFileName, zIndex, 
    marginLeft, position, width, height, you}) => {

        
    const [uploaded, setUploaded] = useState('')

    const filePicker = useRef<HTMLInputElement>(null)

    const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
          const formData = new FormData();
        
          formData.append('AvatarFile', event.target.files[0])
          
          setUploaded(URL.createObjectURL(event.target.files[0]))
          if(you){
            weddingAvatar(formData, true)
          }else{
            weddingAvatar(formData, false)
          }

               
        }
      };

    const handleClick = () => {
        if(filePicker.current){
            filePicker.current.click()
        }
    }

    useEffect(() => {
        // setUploaded(`${import.meta.env.VITE_AVATAR_ROUTE}${avatarFileName}`)
      }, []);

    return (
        <>
            <div onClick={handleClick} className={style.uploadFile} 
                style={{ backgroundImage: `url(${uploaded})`, backgroundSize: 'cover',
                backgroundPosition: 'center', zIndex: zIndex, marginLeft: marginLeft, position: position, 
                width: width, height: height}} >
                {uploaded ? null : '+'}
            </div>
            <input type="file" className={style.hidden}
            accept='image/*,.png,.jpg,.jpeg,' 
            onChange={handleChange}
            ref={filePicker} />
        </>
    );
}