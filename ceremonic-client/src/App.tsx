import { useEffect, useState } from 'react'
import jwtDecode from 'jwt-decode'
import Header from './components/Header'
import Footer from './components/Footer'

import AppRouter from './components/AppRouter'
import { currentDataProvider } from './http/userAPI'
import { useAppDispatch } from './hook'
import { updateIsProvider, updateIsUser } from './store/userSlice'
import { currentDataWedding } from './http/weddingAPI'


function App() {

  const dispatch = useAppDispatch()
  const [avatarProvider, setAvatarProvider] = useState<string>('')
  const [avatarUser, setAvatarUser] = useState<string>('')

  useEffect(() => {
    const token = localStorage.getItem('jwtString')
    if (token) {
      try {
        const decodedToken = jwtDecode(token) as Record<string, unknown>
        if(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] !== 'User'){
          dispatch(updateIsProvider(true))  
          currentDataProvider().then((currentDataProviderResponse) => {
            setAvatarProvider(currentDataProviderResponse.avatarFileName)
          })
        }else{
          dispatch(updateIsUser(true))
          currentDataWedding().then((currentDataWeddingResponse) => {
            setAvatarUser(currentDataWeddingResponse.avatar1FileName)
          })
        } 
      } catch (error) {
        console.log(error)
      }
    } 
    
  }, []);

  return (
    <div className='App'>
      <Header avatarProvider={avatarProvider} avatarUser={avatarUser}/>
      <AppRouter />
      <Footer />
    </div>  
    
  );
}

export default App;