

export interface ICreateItem {
    itemName: string,
    price: number,
    amount: number,
    description: string,
    sellerid: number
}

export interface IDisplayItem {
    id: number,
    itemName: string,
    price: number,
    amount: number,
    description: string
}

export interface IEditItem {
    itemName: string,
    price: number,
    amount: number,
    description: string,
}