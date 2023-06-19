import { createSlice, PayloadAction } from '@reduxjs/toolkit';


type ProviderState = {
    isProvider: boolean,
}

type UserState = {
  isUser: boolean,
  date: string,
}

  const initialState: ProviderState & UserState = {
    isProvider: false,
    isUser: false,
    date: '18.06.2023',

  }
  

const userSlice = createSlice({
    name: 'userInfo',
    initialState,
    reducers: {
        updateIsProvider(state, action: PayloadAction<boolean>) {
            state.isProvider = action.payload

        },
        updateIsUser(state, action: PayloadAction<boolean>) {
          state.isUser = action.payload

        },
        updateDateUser(state, action: PayloadAction<string>) {
          state.date = action.payload

        },
    },
});

export const {updateIsProvider, updateIsUser, updateDateUser} = userSlice.actions;

export default userSlice.reducer;