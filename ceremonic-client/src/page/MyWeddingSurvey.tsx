import DatePick from '../components/DatePick';
import { UploadImageUser } from '../components/UploadImageUser';
import styles from './css/myWeddingSurvey.module.css'
import GEO from '../assets/image/Page_iAmVendor/GEO.svg'
import { useState } from 'react';
import Button from '../components/Button';
import { MY_WEDDING_ROUTE } from '../utils/constRoutes';
import { useNavigate } from 'react-router-dom';
import { weddingEdit } from '../http/weddingAPI';
import { useAppSelector } from '../hook';

const MyWeddingSurvey: React.FC = () => {

    const navigate = useNavigate()
    const [selectedGuestCount, setSelectedGuestCount] = useState<string>('')
    const [selectedCost, setSelectedCost] = useState<string>('')

    const you: boolean = true

    const [fullName1, setFullName1] = useState<string>('')
    const [fullName2, setFullName2] = useState<string>('')

    let date = useAppSelector(state => state.userInfo.date)

    const [isLoading, setIsLoading] = useState<boolean>(true)

    const handleGuestCountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedGuestCount(event.target.value)
        const [min, max] = event.target.value.split(" ") 
    }

    const handleCostChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedCost(event.target.value)
        const cost = event.target.value
    }

    const click = async () => {
        try {
            const formData = new FormData()
            formData.append("FullName1", fullName1);
            formData.append("FullName2", fullName2);
            formData.append("Date", date);
            formData.append("Geolocation", '');
            formData.append("City", '');
            formData.append("GuestCountRange.Min", '0');
            formData.append("GuestCountRange.Max", '0');
            formData.append("ApproximateBudget.Min", '0');
            formData.append("ApproximateBudget.Max", '0');
        
            weddingEdit(formData).then(data => {
                navigate(MY_WEDDING_ROUTE, {replace: true})
            })
                  
        } catch (e: any) {
            alert(e.response.data.message)
        }
    }


  return (
    <div className={styles.myWeddingSurvey}>
        <p className={styles.caption}>Основна інформація про ваше весілля</p>

        <div className={styles.wrapper}>
            <div className={styles.left}>
                <div className={styles.text_input}>
                    <p>
                        Як вас звати?
                    </p>
                    <input className={styles.input} type="text" placeholder='Ввести ім’я' onChange={e => setFullName1(e.target.value)}/>
                </div>

                <p>
                    Завантажте вашу фотографію
                </p>
                <UploadImageUser you={you}/>
            </div>

            <div className={styles.right}>
                <div className={styles.text_input}>
                    <p>
                        Як звати вашого партнера?
                    </p>
                    <input className={styles.input} type="text" placeholder='Ввести ім’я' onChange={e => setFullName2(e.target.value)}/>
                </div>

                <p>
                    Завантажте фотографію партнера
                </p>
                <UploadImageUser />
            </div>
        </div>

        <p>
            На яку дату заплановано весілля?
        </p>
        <div className={styles.datePick}>
            <DatePick /> <br/>
            <div className={styles.inpCheck}>
                <input type="checkbox" id="codex" className={styles.check}/>
                <label htmlFor="codex" className={styles.label_check}>
                    Ще не визначилися з датою
                </label>
            </div>
        </div>

        <p>
            Де відбудеться весілля? Оберіть локацію на мапі.
        </p>
        <div className={styles.datePick}>
            <div className={styles.right_GEOlocation}>
                <div className={styles.right_GEOlocation_icon}>
                  <img src={GEO} alt="icon" />
                </div>
                56.128333, 43.484493
            </div>
            <div className={styles.inpCheck}>
                <input type="checkbox" id="codex" className={styles.check}/>
                <label htmlFor="codex" className={styles.label_check}>
                    Ще не визначилися з локацією
                </label>
            </div>
        </div>

        <p>
            Який у Вас бюджет на весілля?*
        </p>
        <div className={styles.datePick}>
            <div className={styles.cost_input}>
                <div className={styles.right_cost}>
                    <div style={{marginBottom: '38px'}} className={styles.label_before_min}>
                        <input className={styles.right_cost_radio_input} type="radio" name="cost" id="cost_1"
                        value="200000 " checked={selectedCost === '200000 '} onChange={handleCostChange} />
                        <label htmlFor="cost_1" >Мінімальний <span className={styles.cost}>$$</span></label>
                    </div>
                    <div style={{marginBottom: '38px'}} className={styles.label_before_med}>
                        <input className={styles.right_cost_radio_input} type="radio" name="cost" id="cost_2"
                        value="300000" checked={selectedCost === '300000'} onChange={handleCostChange} />
                        <label htmlFor="cost_2" >Оптимальний <span className={styles.cost}>$$$</span></label>
                    </div>
                    <div style={{marginBottom: '38px'}} className={styles.label_before_max}>
                        <input className={styles.right_cost_radio_input} type="radio" name="cost" id="cost_3"
                        value="400000" checked={selectedCost === '400000'} onChange={handleCostChange} />
                        <label htmlFor="cost_3" >Без значних обмежень <span className={styles.cost}>$$$$</span></label>
                    </div>
                </div>
            </div>
            
            <p className={styles.budget}>
                *Тут ми збираємо інформацію, щоб дізнатися ваші вподобання, для подальших рекомендацій. Більш конкретно з 
                бюджетом можна працювати на сторінці Бюджет.
                <br /> <br />
                Зауважте! На традиційне $$ весілля витрачається приблизно 200 000 грн, на $$$ весілля - 230 000 грн до 300 000 грн, а на $$$$ весілля - від 300 000 грн до 400 000 грн, залежно від того, що ви плануєте на весілля, наприклад: буде феєрверк чи ні, і тому подібне.
            </p>
        </div>

        <p>
            Яку кількість гостей орієнтовано бажаєте  запросити на весілля?* 
        </p>
        <div className={styles.quantity}>
            <div className={styles.right_cost}>
                  <div style={{marginBottom: '10px'}}>
                    <input className={styles.right_cost_radio_input} type="radio" name="quantity" id="quantity_1"
                    value="0 19" checked={selectedGuestCount === '0 19'} onChange={handleGuestCountChange} />
                    <label htmlFor="quantity_1">менше 20</label>
                  </div>
                  <div style={{marginBottom: '10px'}}>
                    <input className={styles.right_cost_radio_input} type="radio" name="quantity" id="quantity_2"
                    value="20 50" checked={selectedGuestCount === '20 50'} onChange={handleGuestCountChange} />
                    <label htmlFor="quantity_2">20 - 50</label>
                  </div>
                  <div style={{marginBottom: '10px'}}>
                    <input className={styles.right_cost_radio_input} type="radio" name="quantity" id="quantity_3"
                    value="51 100" checked={selectedGuestCount === '51 100'} onChange={handleGuestCountChange} />
                    <label htmlFor="quantity_3">51 - 100 </label>
                  </div>
                  <div style={{marginBottom: '10px'}}>
                    <input className={styles.right_cost_radio_input} type="radio" name="quantity" id="quantity_4"
                    value="101 150" checked={selectedGuestCount === '101 150'} onChange={handleGuestCountChange} />
                    <label htmlFor="quantity_4">101 - 150 </label>
                  </div>
                  <div>
                    <input className={styles.right_cost_radio_input} type="radio" name="quantity" id="quantity_5"
                    value="151 0" checked={selectedGuestCount === '151 0'} onChange={handleGuestCountChange} />
                    <label htmlFor="quantity_5">більше  150 </label>
                  </div>
            </div>
        </div>

        <Button kind='button_with-background-radius' borderRadius='4px' fontSize='14px' fontWeight={500}
        width='417px' height='40px' onClick={click}>Зберегти</Button>
        <p style={{fontWeight: 300, fontSize: '12px'}}>
            Не хвилюйтесь, у вас буде можливість змінити свій вибір.
        </p>

    </div>
  );
}

export default MyWeddingSurvey