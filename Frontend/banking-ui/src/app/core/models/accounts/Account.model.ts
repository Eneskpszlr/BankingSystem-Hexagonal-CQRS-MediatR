export interface Account {
    id: number;
    accountNumber: string;
    customerId: number;
    branchId: number;

    // --- Value Object Flattening ---
    balance: number;
    currencyCode: string;
    status: string;

    firstName: string;
    lastName: string;
    identityNumber: string;
    email: string;
    phone: string;
    
    street: string;
    city: string;
    country: string;
    zipCode: string;
    
    createdDate?: string;
}

