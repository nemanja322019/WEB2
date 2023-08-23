import { UserTypes, VerificationStatus } from "../Enums/enumerations"

export interface IUserLogin {
    email: string,
    password: string
}

export interface IUserRegister {
    username: string,
    email: string,
    password: string,
    name: string,
    lastName: string,
    birthDate: Date,
    address: string,
    userType: UserTypes
}

export interface IUserProfile {
    id: number,
    username: string,
    email: string,
    name: string,
    lastName: string,
    birthDate: Date,
    address: string,
    isVerified: boolean,
    verificationStatus: VerificationStatus,
    userType: UserTypes
}

export interface IUserUpdate{
    name: string,
    lastName: string,
    birthDate: Date,
    address: string
}

export interface IUserPasswordChange{
    oldpassword: string,
    newpassword: string
}

export interface IGoogleToken {
    token: string
}
