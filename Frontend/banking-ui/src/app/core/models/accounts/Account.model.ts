export interface Account {
    id: number;
    customerId: number;
    branchId: number;
    firstName: string;
    lastName: string;
    identityNumber: string;
    email: string;
    phone: string;
    balance: number;
    currencyCode: string;  
    
    street: string;
    city: string;
    country: string;
    zipCode: string;
    
    createdDate?: string;
    status: string;
}

