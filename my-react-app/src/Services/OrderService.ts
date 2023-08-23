import { axiosClient } from "./axiosClient";
import { API } from "../Constants/Constants";
import { INewOrder } from "../Shared/Interfaces/orderInterfaces";

export const NewOrder =async (newOrder: INewOrder) => {
    return await axiosClient.post(`${API}/orders`, newOrder);
}

export const CancelOrder =async (id: number) => {
    return await axiosClient.put(`${API}/orders/${id}/cancel`);
}

export const GetAllOrders =async () => {
    return await axiosClient.get(`${API}/orders/admin/all`);
}

export const GetNewOrdersFromSeller =async (id: number) => {
    return await axiosClient.get(`${API}/orders/seller/${id}/new`);
}

export const GetOldOrdersFromSeller =async (id: number) => {
    return await axiosClient.get(`${API}/orders/seller/${id}/old`);
}

export const GetOngoingOrdersFromCustomer =async (id: number) => {
    return await axiosClient.get(`${API}/orders/${id}/ongoing`);
}

export const GetDeliveredOrdersFromCustomer =async (id: number) => {
    return await axiosClient.get(`${API}/orders/${id}/delivered`);
}