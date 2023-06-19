import styles from './css/allVendors.module.css'
import head from '../assets/image/Page_AllVendor/head.png'
import close from '../assets/image/Page_MyWedding/container.svg'
import GEO from '../assets/image/Page_iAmVendor/GEO.svg'
import Heart from '../assets/image/Page_AllVendor/heart.svg'
import Loc from '../assets/image/Page_AllVendor/loc.svg'

import { ReactComponent as People } from '../assets/image/Page_AllVendor/people.svg'
import { ReactComponent as Tent } from '../assets/image/Page_AllVendor/tent.svg'
import { useEffect, useState } from 'react'
import { providerAll } from '../http/userAPI'

type Provider = {
  userId: number,
  firstName: string,
  lastName: string,
  serviceName: string,
  brandName: string,
  avatarFileName: string,
    imageFileNames: string[],
    info: string,
    placeName: string,
    geolocation: string,
    city: string,
    averagePrice: {
      min: number,
      max: number,
    },
    workingDayList: string
}[] 

const AllVendors: React.FC = () => {

  const [isLoading, setIsLoading] = useState<boolean>(true)

  const [checkboxes, setCheckboxes] = useState({
    checkbox1: false,
    checkbox2: false,
    checkbox3: false,
    checkbox4: false,
    checkbox5: false,
    checkbox6: false,
    checkbox7: false,
    checkbox8: false,
    checkbox9: false,
    checkbox10: false,
    checkbox11: false,
    checkbox12: false,
    checkbox13: false,
    checkbox14: false,
    checkbox15: false,
    checkbox16: false,
    checkbox17: false,
  });

  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, checked } = event.target;
    setCheckboxes((prevCheckboxes) => ({
      ...prevCheckboxes,
      [name]: checked,
    }));
  };

  const [objects, setObjects] = useState<Provider>([])

  useEffect(() => {
    providerAll('', [''], '', '', 0, '', 0).then((data) => {
      setObjects(data)
      setIsLoading(false)
    })

    
  }, [])


  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className={styles.allVendors}>
      <img src={head} alt="" />
        
      <p className={styles.text_caption}>
        Давайте підберемо вашу весільну команду!
      </p>

      <div className={styles.wrap_tent}>
        <div className={styles.people_tent}>
          <People className={styles.vector}/>
          <span>Послуги</span>
          <span style={{color: '#979797', marginLeft: '30px'}}>15</span>
        </div>
        <div className={styles.people_tent} style={{borderRadius: '0px 4px 4px 0px'}}>
          <Tent className={styles.vector}/>
          <span>Місця</span>
          <span style={{color: '#979797', marginLeft: '30px'}}>20</span>
        </div>

      </div>

      <div className={styles.main}>
          <div className={styles.left}>
            <div className={styles.search}>
              <p>Пошук постачальника</p>
              <div className={styles.inp_search}>Ввести пошуковий запит</div>

            </div>

            <div className={styles.sort}>
              <p>Сортувати</p>
              <div className={styles.inp_search}>Сортувати за</div>
              <p>Відобразити</p>

              <label>
                <input
                  type="checkbox"
                  name="checkbox1"
                  checked={checkboxes.checkbox1}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Усі</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox2"
                  checked={checkboxes.checkbox2}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Цермоніймейстр</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox3"
                  checked={checkboxes.checkbox3}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Ведучий</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox4"
                  checked={checkboxes.checkbox4}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Їжа/кайтеринг</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox5"
                  checked={checkboxes.checkbox5}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Флористика та декор</span> 
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox6"
                  checked={checkboxes.checkbox6}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Декор та освіщення</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox7"
                  checked={checkboxes.checkbox7}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Поліграфія</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox8"
                  checked={checkboxes.checkbox8}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Кондитер</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox9"
                  checked={checkboxes.checkbox9}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Фотозйомка</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox10"
                  checked={checkboxes.checkbox10}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Відеозйомка</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox11"
                  checked={checkboxes.checkbox11}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Музика</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox12"
                  checked={checkboxes.checkbox12}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Технічна підтримка</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox13"
                  checked={checkboxes.checkbox13}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Візажист</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox14"
                  checked={checkboxes.checkbox14}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Перукар</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox15"
                  checked={checkboxes.checkbox15}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Автомобіль наречених</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox16"
                  checked={checkboxes.checkbox16}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Транспорт для гостей</span>
              </label>

              <label>
                <input
                  type="checkbox"
                  name="checkbox17"
                  checked={checkboxes.checkbox17}
                  onChange={handleCheckboxChange}
                  style={{marginLeft: '0px'}}
                />
                <span style={{marginLeft: '8px'}}>Букет нареченої</span>
              </label>
              
              <p style={{marginTop: '30px', marginBottom: '5px'}}>Подробиці:</p>
              <p>Дата вашого весілля:</p>

              <div className={styles.day}>ДД/ММ/РРРР <img src={close} alt="x" /> </div>
             

              <p>Оберіть приблизну локацію <br /> вашого весілля:</p>
              <div style={{boxSizing: 'border-box', width: '263px', height: '40px', border: '1px solid #979797', 
                  borderRadius: '5px', paddingTop: '9px', paddingLeft: '15px'}}>
                <img src={GEO} alt="icon" />
              </div>

              <p style={{marginTop: '25px', marginBottom: '5px'}}>Цінова категорія:</p>
              <div className={styles.day} style={{fontWeight: 500, fontSize: '14px', lineHeight: '17px', 
                color: '#66969C', marginBottom: '35px'}}>$$$ 
              <img src={close} alt="x" /> </div>

              <div className={styles.but}>
                Пошук
              </div>


              <div className={styles.och}>
                Очистити
              </div>

            </div>

          </div>


          <div className={styles.right}>
                {objects && objects.map(obj => (
                  <div className={styles.ven_wrap} key={obj.userId}>

                    <div className={styles.wrap_left}>
                      <div style={{background: `url(${import.meta.env.VITE_AVATAR_ROUTE}${obj.avatarFileName})`, 
                      borderRadius: '5px', width: '185px', height: '139px'}}>
                      </div>

                      <div className={styles.wrap_left_bot}>
                        <div className={styles.wrap_left_bot_text}>
                          <span>Залишити</span> 
                          <span>повідомлення</span>
                        </div>

                        <img src={Heart} style={{width: '40px', height: '40px'}}/>

                      </div>
                    </div>

                    <div className={styles.wrap_right} >
                      <p>{obj.firstName} {obj.lastName}, {obj.serviceName} {obj.averagePrice.min === 0 ? 
                        <span style={{fontWeight: 500, fontSize: '14px', lineHeight: '17px', color: '#66969C'}}>$$</span>
                      :
                        obj.averagePrice.min === 1700 ?
                            <span style={{fontWeight: 500, fontSize: '14px', lineHeight: '17px', color: '#66969C'}}>$$$</span>
                          :
                            <span style={{fontWeight: 500, fontSize: '14px', lineHeight: '17px', color: '#66969C'}}>$$$$</span>}</p>
                      <div>
                        <img src={Loc} />
                        {obj.city}
                      </div>
                      <p>{obj.info}</p>

                      <p className={styles.text_bot}>Перейти на сторінку</p>
                    </div>
                  </div>
                ))}
              
            
          </div>

        </div>




    </div>
  );
}

export default AllVendors