export interface CreateCustomerRequest {
    firstName: string;
    lastName: string;
    identityNumber: string;
    email: string;
    phone: string;

    street: string;
    city: string;
    country: string;
    zipCode: string;
}