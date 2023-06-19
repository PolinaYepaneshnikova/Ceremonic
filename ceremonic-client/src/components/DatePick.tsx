import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider"
import { DatePicker } from "@mui/x-date-pickers/DatePicker"
import React, { useState } from "react"
import dayjs, { Dayjs } from "dayjs"
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import 'dayjs/locale/uk';
import { useAppDispatch } from "../hook";
import { updateDateUser } from "../store/userSlice";

const DatePick: React.FC = () => {

  const [date, setDate] = useState<Dayjs | null>(dayjs())

  const dispatch = useAppDispatch()

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='uk'>
      <DatePicker sx={{'& .MuiOutlinedInput-notchedOutline': { borderColor: '#00889A', borderWidth: '2px' },
      '& .MuiFormLabel-root': { color: '#00889A', 
    }}}
      label="ДД/ММ/РРРР" format="DD/MM/YYYY" value={date} onChange={(newValue) => 
      {
        setDate(newValue)

        if(newValue){
          let month: string = '0'
          let  D: number = newValue.toDate().getDate()
          let  M: number = newValue.toDate().getMonth() + 1
          let  Y: number = newValue.toDate().getFullYear()
          if(M < 10){
            month = month+M
            dispatch(updateDateUser(`${Y}-${month}-${D}T12:00:00Z`))
          }else{
            dispatch(updateDateUser(`${Y}-${M}-${D}T12:00:00Z`))
          }
          
        }
        

      }}/>
    </LocalizationProvider>
  )
}

export default DatePick