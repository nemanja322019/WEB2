

export interface ICreateItem {
    itemName: string,
    price: number,
    amount: number,
    description: string,
    sellerid: number,
    image: string
}

export interface IDisplayItem {
    id: number,
    itemName: string,
    price: number,
    amount: number,
    description: string,
    image: string
}

export interface IEditItem {
    itemName: string,
    price: number,
    amount: number,
    description: string,
    image: string
}