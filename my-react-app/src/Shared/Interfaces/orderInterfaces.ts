import { OrderStatus } from "../Enums/enumerations"
import { IDisplayItem } from "./itemInterfaces"

export interface IOrderItem{
    itemId: number,
    amount: number
}

export interface INewOrder {
    comment: string,
    address: string,
    customerId: number,
    orderItems: IOrderItem[]
}

export interface IOrder {
    id: number,
    comment: string,
    address: string,
    price: number,
    orderTime: string,
    deliveryTime: string,
    isCanceled: boolean,
    status: string,
    orderItems: IDisplayItem[]
}