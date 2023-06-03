import { useEffect, useState } from 'react';
import './css/authProvider.css'

import {useLocation, useNavigate} from "react-router-dom";
import { VENDOR_ROUTE, LOGIN_PROVIDER_ROUTE, REGISTRATION_PROVIDER_ROUTE} from "../utils/constRoutes";

import { userLogin, providerRegistration, providerRoles, providerGoogleRegistration } from '../http/userAPI';

import Input from '../components/Input'
import Button from '../components/Button'
import AuthProviderSteps from '../components/AuthProviderSteps'
import MyGoogleLogin from '../components/MyGoogleLogin'
import { useAppDispatch, useAppSelector } from '../hook'
import { addArrayServiceName } from '../store/authProviderSlice'
import { updateIsProvider } from '../store/userSlice';

type Provider = {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    brandName: string,
    serviceName: string,
    tokenID: string,
} 

const AuthProvider: React.FC = () => {

    const dispatch = useAppDispatch()

    const provider: Provider = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        brandName: '',
        serviceName: '',
        tokenID: '',
    }
    provider.serviceName = useAppSelector(state => state.authProviderInfo.authProvider.serviceName)
    provider.firstName = useAppSelector(state => state.authProviderInfo.authProvider.firstName)
    provider.lastName = useAppSelector(state => state.authProviderInfo.authProvider.lastName)
    provider.email = useAppSelector(state => state.authProviderInfo.authProvider.email)
    provider.password = useAppSelector(state => state.authProviderInfo.authProvider.password)
    provider.brandName = useAppSelector(state => state.authProviderInfo.authProvider.brandName)
    provider.tokenID = useAppSelector(state => state.authProviderInfo.authProvider.tokenID)

    const [currentStep, setCurrentStep] = useState<number>(1)
    const [isChecked, setIsChecked] = useState<boolean>(false)

    const navigate = useNavigate()
    const location = useLocation()
    const isLogin = location.pathname === LOGIN_PROVIDER_ROUTE

    const [email, setEmail] = useState<string>('')
    const [password, setPassword] = useState<string>('')

    const fetchData = async () => {
        const {data}: any = await providerRoles();
        dispatch(addArrayServiceName(data))     
    };

    useEffect(() => {
        fetchData()
        
    }, [])

    const click = async () => {
        try {
            let data;
            if (isLogin) {
                data = await userLogin(email, password);
                if(data){
                    dispatch(updateIsProvider(true))
                    navigate(VENDOR_ROUTE, {replace: true})
                }
            }
            else if(isChecked){
                
                if(provider.tokenID !== ""){
                    data = await providerGoogleRegistration(provider.firstName, provider.lastName, provider.tokenID, 
                        provider.serviceName, provider.brandName);
                }else {
                    data = await providerRegistration(provider.firstName, provider.lastName, provider.email, provider.password, 
                        provider.serviceName, provider.brandName);
                }
                dispatch(updateIsProvider(true))
                // user.setUser(data)
                // user.setIsAuth(true)
                navigate(VENDOR_ROUTE, {replace: true})
            }else{
                alert("НАТИСНИ, ЩО ЗГОДЕН!!!!")
            }
        } catch (e: any) {
            alert(e.response.data.message)
        }

    }

  return (
    <div className="authProvider">
        {!isLogin ?
                <div className="authProvider__registration">
                    <p className='authProvider__caption'>
                        Реєстрація постачальника
                    </p>

                    <p className='authProvider__caption-step'>
                        Крок {currentStep}.
                    </p>

                    <AuthProviderSteps currentStep={currentStep} setCurrentStep={setCurrentStep} 
                    isChecked={isChecked} 
                    setIsChecked={setIsChecked}/>

                    {currentStep !== 3 ?
                        <Button kind='button_with-shadow' color='#000000' width='208px' 
                        height='36px' borderRadius='5px'
                        fontSize='16px' fontWeight={500} lineHeight='20px'
                        onClick={() => {
                            currentStep === 2 ? 
                                setCurrentStep(3)
                            :
                                setCurrentStep(2)}}>
                            Далі
                        </Button>
                    :
                        <Button kind='button_with-shadow' color='#000000' width='208px' 
                        height='36px' borderRadius='5px'
                        fontSize='16px' fontWeight={500} lineHeight='20px'
                        onClick={click}>
                            Зареєструватися
                        </Button>
                    }
                    <div className='authProvider__login-buttohttp://localhost:5173/about'>
                        <span className='authProvider__question'>Вже зареєстровані?</span>
                        <Button kind='button_secondary' onClick={() => navigate(LOGIN_PROVIDER_ROUTE, {replace: true})}
                        fontWeight={400} fontSize='10px' lineHeight='12px'>Увійдіть</Button>
                    </div>

                </div>
            :
                <div className="authProvider__registration">
                    <p className='auth__caption'>
                        Вхід постачальника
                    </p>
                    <div className='auth__registration-button'>
                        <span className='auth__question auth__question-login'>Ще не зареєстровані?</span>
                        <Button kind='button_secondary' onClick={() => navigate(REGISTRATION_PROVIDER_ROUTE, {replace: true})}>
                            Зареєструйтеся зараз</Button>
                    </div>

                    <MyGoogleLogin />

                    <p className='auth__text-pre auth__text-pre-login'>
                        Або за допомогою пошти
                    </p>
                    <div className="auth__registration-inputs auth__registration-inputs-login authProvider__inputs">
                        <Input kind='input_without-border' type='email' placeholder='Email'
                        value={email}
                        onChange={e => setEmail(e.target.value)}/>
                        <Input kind='input_without-border' type='password' placeholder='Пароль'
                        value={password}
                        onChange={e => setPassword(e.target.value)}/>
                        
                    </div>
                    <Button kind='button_with-shadow' color='#000000' width='208px' height='36px' borderRadius='5px'
                    fontSize='16px' fontWeight={600} lineHeight='20px'
                    onClick={click}>
                        Вхід
                    </Button>

                </div>
        }
    </div>
  );
}

export default AuthProvider;
