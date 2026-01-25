export interface UpdateCustomerRequest {
    id: number;
    email: string;
    phone: string;

    street: string;
    city: string;
    country: string;
    zipCode: string;
}