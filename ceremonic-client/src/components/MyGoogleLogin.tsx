import { GoogleLogin } from "@react-oauth/google"
import jwtDecode from 'jwt-decode'
import { userGoogleRegistration, userGoogleLogin, providerGoogleRegistration } from '../http/userAPI';
import { useLocation, useNavigate } from 'react-router-dom';
import { LOGIN_ROUTE, REGISTRATION_PROVIDER_ROUTE, LOGIN_PROVIDER_ROUTE, VENDOR_ROUTE, MY_WEDDING_SURVEY_ROUTE, MY_WEDDING_ROUTE } from '../utils/constRoutes';
import { useAppDispatch } from '../hook';
import { addGoogleRegistration } from '../store/authProviderSlice';
import { updateIsProvider, updateIsUser } from '../store/userSlice';

type ChildrenProps = {
    setCurrentStep?: (step: number) => void;
}

const MyGoogleLogin: React.FC<ChildrenProps> = ({setCurrentStep}) => {

    const dispatch = useAppDispatch()
    const navigate = useNavigate()
    const location = useLocation()
    const isLogin: boolean = location.pathname === LOGIN_ROUTE
    const isProviderLogin: boolean = location.pathname === LOGIN_PROVIDER_ROUTE
    const isProviderRegistration: boolean = location.pathname === REGISTRATION_PROVIDER_ROUTE

    const login = async (credentialResponse: any) => {
        try {
            const tokenID = credentialResponse.credential.toString()
            const decodeJWT: any = jwtDecode(tokenID ?? '')
            let data;         
            data = await userGoogleLogin(tokenID)
            const token = localStorage.getItem('jwtString')
            
            if (isLogin || isProviderLogin) {
                if(token){
                    const decodedToken = jwtDecode(token) as Record<string, unknown>               
                
                    if(isProviderLogin && (decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] !== 'User')){
                        dispatch(updateIsProvider(true))
                        navigate(VENDOR_ROUTE, {replace:true})
                        return
                    }
                    dispatch(updateIsUser(true))
                    navigate(MY_WEDDING_ROUTE, {replace: true})
                    return
                }
            } 
            else if(isProviderRegistration){
                if (typeof setCurrentStep === 'function') {
                    setCurrentStep(2)
                } 
                dispatch(addGoogleRegistration(tokenID))
                return
            }
            else {
                data = await userGoogleRegistration(decodeJWT.given_name, decodeJWT.family_name, tokenID);
            }
            navigate(MY_WEDDING_SURVEY_ROUTE, {replace: true})
        } catch (e: any) {
            alert(e.response.data.message)
        }

    }

  return (
    <GoogleLogin
        locale="uk"
        onSuccess={credentialResponse => {
            login(credentialResponse)
            
        }}
        onError={() => {
            console.log('Login Failed');
        }}
        useOneTap
    />
    )

}

export default MyGoogleLogin;


