import {$authHost, $host} from "./index";
import jwt_decode from "jwt-decode"


export const weddingEdit = async (formData: FormData) => {
    const {data} = await $authHost.put('/api/Weddings/edit', formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
    return data
}


export const weddingAvatar = async (formData: FormData, isMyAvatar: boolean) => {
    const {data} = await $authHost.put(`/api/Weddings/editAvatar/${isMyAvatar}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
    return data
}

export const currentDataWedding = async () => {
    const {data} = await $authHost.get('/api/Weddings/currentWedding')
    return data
}