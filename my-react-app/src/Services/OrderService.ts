import { axiosClient } from "./axiosClient";
import { API } from "../Constants/Constants";
import { INewOrder } from "../Shared/Interfaces/orderInterfaces";

export const NewOrder =async (newOrder: INewOrder) => {
    return await axiosClient.post(`${API}/orders`, newOrder);
}