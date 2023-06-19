import { UploadImageUser } from '../components/UploadImageUser';
import styles from './css/myWedding.module.css'
import { ReactComponent as Vector } from '../assets/image/Page_MyWedding/pencil.svg'
import pencil from '../assets/image/Page_MyWedding/pencil.svg'
import close from '../assets/image/Page_MyWedding/container.svg'

import tent from '../assets/image/Page_MyWedding/tent.svg'
import table from '../assets/image/Page_MyWedding/table-dinner.svg'
import waiter from '../assets/image/Page_MyWedding/food-dish-waiter.svg'
import arch from '../assets/image/Page_MyWedding/wedding-arch.svg'
import flowers from '../assets/image/Page_MyWedding/flowers.svg'
import garlands from '../assets/image/Page_MyWedding/garlands.svg'
import search from '../assets/image/Page_MyWedding/search.svg'


import { useNavigate } from 'react-router-dom';
import { MY_WEDDING_SURVEY_ROUTE } from '../utils/constRoutes';
import Timer from '../components/Timer';
import { useEffect, useState } from 'react';
import { currentDataWedding } from '../http/weddingAPI';

const MyWedding: React.FC = () => {

  const navigate = useNavigate()

  const [time, setTime] = useState<number>(2592000)

  const ta = ' та '

  const [name1, setName1] = useState<string>('')
  const [name2, setName2] = useState<string>('')
  const [date, setDate] = useState<string>()
  const [avatar1FileName, setAvatar1FileName] = useState<string>('')
  const [avatar2FileName, setAvatar2FileName] = useState<string>('')

  const [isLoading, setIsLoading] = useState<boolean>(true)

  function getSecondsDifference(date1: string): number {

    const today = new Date()

    const wedding = new Date(date1)

    const diffInMilliseconds = Math.abs(wedding.getTime() - today.getTime())
    const seconds = Math.floor(diffInMilliseconds / 1000)
    return seconds
  }

  useEffect(() => {
    currentDataWedding().then((currentDataWeddingResponse) => {
      setName1(currentDataWeddingResponse.fullName1)
      setName2(currentDataWeddingResponse.fullName2)
      const dateTimeString = currentDataWeddingResponse.date
      const date = new Date(dateTimeString)
      const formattedDate = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`
      setDate(formattedDate)
      setTime(getSecondsDifference((currentDataWeddingResponse.date)))
      setAvatar1FileName(currentDataWeddingResponse.avatar1FileName)
      setAvatar2FileName(currentDataWeddingResponse.avatar2FileName)
      setIsLoading(false)
    })
    
  }, [])

  if (isLoading) {
    return <div>Loading...</div>;
  }


  return (
    <div className={styles.myWedding}>
      <Vector className={styles.pencil} onClick={() => navigate(MY_WEDDING_SURVEY_ROUTE, {replace: true})}/>
      <p className={styles.caption}> Весілля {name1} {ta} {name2}</p>

      <div className={styles.photo}>
          <div className={styles.uploadImageContainer} style={{ zIndex: 10 }}>
            <div className={styles.imageContainerBig} style={{
              backgroundImage: `url(${import.meta.env.VITE_AVATAR_ROUTE}${avatar1FileName})`, backgroundSize: 'cover',
                backgroundPosition: 'center'}}>
            </div>
          </div>

          <div className={styles.uploadImageContainer} style={{ zIndex: 1, marginLeft: '123px' }}>
            <div className={styles.imageContainerBig} style={{
              backgroundImage: `url(${import.meta.env.VITE_AVATAR_ROUTE}${avatar2FileName})`, backgroundSize: 'cover',
                backgroundPosition: 'center'}}>
            </div>
          </div>
      </div>

      <p style={{fontSize: '24px', lineHeight: '29px'}}>
        {name1} {ta}
        <span style={{color: '#66969C'}}>{name2}</span></p>
      <p style={{fontWeight: 300, fontSize: '24px', lineHeight: '29px'}}>{date}</p>
      <p style={{fontWeight: 300, fontSize: '16px', lineHeight: '20px'}}>До вашого весілля залишилося 
      <span style={{marginLeft: '30px'}}>
        <Timer duration={time}/>
      </span> </p>

      <div className={styles.services}>
        <div className={styles.subdivisions}>
          <span><span className={styles.zero}>0</span> <span className={styles.numbers}>з 18</span></span> 
          <span>Послуг</span> 
          замовлено
        </div>

        <div className={styles.subdivisions}>
          <span><span className={styles.zero}>0</span> <span className={styles.numbers}>з 60</span></span>
          <span>Завдань</span>
          виконано
        </div>

        <div className={styles.subdivisions}>
          <span><span className={styles.zero}>0</span> <span className={styles.numbers}>з 50</span></span>
          <span>Гостей</span>
          прийде
        </div>

        <div className={styles.subdivisions}>
          <span><span className={styles.zero}>0</span> <span className={styles.numbers}>з 60</span></span>
          <span>Гостей</span>
          розсаджено
        </div>

      </div>

      <div className={styles.line}>
      </div>

      <p className={styles.caption}>Почніть планувати своє весілля</p>

      <div className={styles.boxes}>
        <div className={styles.wrapper_caption}>
          <span className={styles.caption_in_wrapper}>Поточні завдання</span>
          <img src={pencil} alt="Pencil" />
        </div>

        <div className={styles.tasks}>
            <p style={{marginTop: '37px', marginBottom: '67px'}}>На даний момент поточних завдань немає.</p>
            <span>Розпочніть планування весілля з планера завдань. </span>
            <span style={{marginBottom: '65px'}}>Для цього перейдіть на усі завдання.</span>
            <span style={{fontSize: '16px', lineHeight: '20px', color: '#00889A', marginBottom: '25px'}}>Усі мої завдання</span>
            <span style={{fontSize: '16px', lineHeight: '20px', color: '#66969C'}}>Згорнути</span>
        </div>

      </div>

      <div className={styles.boxes_even} style={{height: '411px'}}>
        <div className={styles.wrapper_caption}>
          <span className={styles.caption_in_wrapper}>План весілля</span>
          <img src={pencil} alt="Pencil" />
        </div>

        <div className={styles.wrap}>
          <div className={styles.plan}>
            <div>Година</div>         <div>Подія</div>                   <div>Нотатки</div>
            <div className={styles.block_time} style={{color: '#00889A'}}>8.00 - 9.00</div>    <div className={styles.block_rest}>Прокинутися, сніданок</div>   <div className={styles.block_rest}>Вдома</div>
            <div className={styles.block_time} style={{color: '#00889A'}}>9.00 - 10.00</div>   <div className={styles.block_rest}>Макіяж</div>                  <div className={styles.block_rest}>Візажист приїде додому</div>
            <div></div>               <div className={styles.block_rest}>Зачіска</div>                 <div className={styles.block_rest}>Визначитися з зачіскою!!!</div>
          </div>
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#00889A', marginBottom: '25px'}}>Відкрити план дня</span>
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#66969C'}}>Згорнути</span>
        </div>
      </div>

      <div className={styles.boxes} style={{height: '358px'}}>
        <div className={styles.wrapper_caption}>
          <span className={styles.caption_in_wrapper}>Бюджет</span>
          <img src={pencil} alt="Pencil" />
        </div>

        <div className={styles.wrap}>
          <div className={styles.budget}>
            <div style={{fontSize: '14px',lineHeight: '17px', marginBottom: '40px', color: '#000000'}}>Записати витрату:</div> <div></div> <div></div> <div></div> <div></div> <div></div>
            <div className={styles.budget_caption}>Грошей доступно</div> <div className={styles.budget_caption}>Сума витрати</div> 
            <div className={styles.budget_caption}>Призначення</div> <div className={styles.budget_caption}>Категорія</div> 
            <div className={styles.budget_caption}>Статус оплати</div> <div></div>
            <div style={{display: 'flex', justifyContent: 'center', borderBottom: '1px solid #979797', alignItems: 'end', fontSize: '14px',
                lineHeight: '17px'}}>0 грн</div> 
            <div className={styles.budget_div}>Ввести сумму</div> <div className={styles.budget_div}>Ввести призначення</div> 
            <div className={styles.budget_div_close}>Оберіть категорію <img src={close} alt="x" /> </div> 
            <div className={styles.budget_div_close}>Статус оплати <img src={close} alt="x" /> </div> 
            <div className={styles.budget_button}>Додати</div>
          </div>
  
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#00889A', marginBottom: '25px'}}>Мій бюджет</span>
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#66969C'}}>Згорнути</span>
        </div>
      </div>

      <div className={styles.boxes_even} >
        <div className={styles.wrapper_caption}>
          <span className={styles.caption_in_wrapper}>Гості</span>
          <img src={pencil} alt="Pencil" />
        </div>


        <div className={styles.wrap}>
          <div className={styles.guest}>
            <div className={styles.guest_one}>
              <p style={{display: 'flex', justifyContent: 'space-between', marginRight: '100px'}}>Всього гостей <span className={styles.guest_one_count}>90</span></p>
              <p style={{display: 'flex', justifyContent: 'space-between', marginRight: '100px'}}>Прийдуть <span className={styles.guest_one_count}>45</span></p>
              <p style={{display: 'flex', justifyContent: 'space-between', marginRight: '100px'}}>Не прийдуть <span className={styles.guest_one_count}>45</span></p>
            </div>

            <div className={styles.guest_two}>
              <div className={styles.guest_two_in}>
                <div className={styles.imageContainerMini} style={{
                  backgroundImage: `url(${import.meta.env.VITE_AVATAR_ROUTE}${avatar1FileName})`, backgroundSize: 'cover',
                  backgroundPosition: 'center'}}>
                </div>
                <p className={styles.under_photo}>Гості зі сторони Сімони</p>
                <p className={styles.guest_photo_count}>67</p>
              </div>

              <div className={styles.guest_two_in}>
                <div className={styles.imageContainerMini} style={{
                  backgroundImage: `url(${import.meta.env.VITE_AVATAR_ROUTE}${avatar2FileName})`, backgroundSize: 'cover',
                  backgroundPosition: 'center'}}>
                </div>
                <p className={styles.under_photo}>Гості зі сторони Сімони</p>
                <p className={styles.guest_photo_count}>23</p>
              </div>
            </div>
          </div>

          <span style={{fontSize: '16px', lineHeight: '20px', color: '#00889A', marginBottom: '25px'}}>Всі мої  гості</span>
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#66969C'}}>Згорнути</span>
        </div>
      </div>
      
      <div className={styles.big_box}>
        <div className={styles.wrapper_caption}>
          <span className={styles.caption_in_wrapper}>Моя весільна  команда </span>
          <img src={pencil} alt="Pencil" />
        </div>

        <div className={styles.wrap}>
          <div className={styles.vendor}>
            <div className={styles.vendor_div}>
              <img src={tent} alt="" />
              <p className={styles.vendor_name}>
                Місце проведення церемонії
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>

            <div className={styles.vendor_div}>
              <img src={table} alt="" />
              <p className={styles.vendor_name}>
                Банкетний зал
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>

            <div className={styles.vendor_div}>
              <img src={waiter} alt="" />
              <p className={styles.vendor_name}>
                Кейтеринг
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>

            <div className={styles.vendor_div}>
              <img src={arch} alt="" />
              <p className={styles.vendor_name}>
                Церемоніймейстер
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>

            <div className={styles.vendor_div}>
              <img src={flowers} alt="" />
              <p className={styles.vendor_name}>
                Флористика
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>

            <div className={styles.vendor_div}>
              <img src={garlands} alt="" />
              <p className={styles.vendor_name}>
                Декор та освіщення
              </p>
              <div className={styles.vendor_search}>
                <img src={search} alt="" style={{marginRight: '20px'}}/> Пошук
              </div>
            </div>
          </div>

          <div style={{width: '100%', color: '#49454F', fontSize: '16px', lineHeight: '20px', boxSizing: 'border-box',
            marginTop: '10px', marginBottom: '52px', display: 'flex', justifyContent: 'end', paddingRight: '86px'}}>
            Усі постачальники
          </div>

          <span style={{fontSize: '16px', lineHeight: '20px', color: '#00889A', marginBottom: '25px'}}>Всі мої постачальники</span>
          <span style={{fontSize: '16px', lineHeight: '20px', color: '#66969C'}}>Згорнути</span>
        </div>
      </div>
      
      

      <div className={styles.footer}>
        <p>
          Пізнай можливості сервісу Ceremonic краще
        </p>

        <div className={styles.footer_text}>
          <span style={{marginRight: '140px'}}>
            Читай статті та збирай корисні <br /> поради
          </span>
           
          <span style={{marginRight: '140px'}}>
            Спілкуйся з постачальником <br /> послуги у месенджері
          </span>

          <span style={{marginRight: '160px'}}>
            Пройди тест на колір твого <br /> весілля
          </span>

          <span>
            Створи схему розсадки гостей
          </span>

        </div>

      </div>
      
    </div>
  );
}

export default MyWedding;
