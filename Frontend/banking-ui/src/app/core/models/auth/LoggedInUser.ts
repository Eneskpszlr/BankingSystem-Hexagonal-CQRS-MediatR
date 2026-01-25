// KULLANICI DURUMU (Uygulama içinde kullanacağımız model)
export interface LoggedInUser {
    id: number;
    fullName: string;
    roles: string[];
    isAdmin: boolean;
}
