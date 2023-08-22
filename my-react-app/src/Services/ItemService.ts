import { axiosClient } from "./axiosClient";
import { API } from "../Constants/Constants";
import { ICreateItem, IEditItem } from "../Shared/Interfaces/itemInterfaces";

export const CreateItem =async (createItem: ICreateItem) => {
    return await axiosClient.post(`${API}/items`, createItem);
}

export const EditItem =async (id: number, editItem: IEditItem) => {
    return await axiosClient.put(`${API}/items/${id}`, editItem);
}

export const GetItemsForSeller =async (id: number) => {
    return await axiosClient.get(`${API}/items/seller/${id}`);
}

export const DeleteItem =async (id: number) => {
    return await axiosClient.delete(`${API}/items/${id}`);
}

export const GetItems =async () => {
    return await axiosClient.get(`${API}/items/`);
}