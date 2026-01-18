import { environment } from "../../../environments/environment";

// Backend API URL'ini temel alıyoruz
const BASE_URL = environment.apiUrl;

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: `${BASE_URL}/auth/login`,
    REGISTER: `${BASE_URL}/auth/register`,
    REFRESH_TOKEN: `${BASE_URL}/auth/refresh-token`
  },

  // --- ACCOUNT (Hesap İşlemleri) ---
  ACCOUNTS: {
    BASE: `${BASE_URL}/accounts`,
    GET_BY_ID: (id: number) => `${BASE_URL}/accounts/${id}`,
    GET_BY_CUSTOMER: (customerId: number) => `${BASE_URL}/accounts/customer/${customerId}`
  },

  // --- TRANSACTION (Finansal İşlemler) ---
  TRANSACTIONS: {
    BASE: `${BASE_URL}/transactions`,
    GET_BY_ID: (id: number) => `${BASE_URL}/transactions/${id}`,
    
    // İşlem Türleri
    DEPOSIT: `${BASE_URL}/transactions/deposit`,
    WITHDRAW: `${BASE_URL}/transactions/withdraw`,
    TRANSFER: `${BASE_URL}/transactions/transfer`
  },

  // --- CUSTOMER (Müşteri Yönetimi) ---
  CUSTOMERS: {
    BASE: `${BASE_URL}/customers`,
    GET_BY_ID: (id: number) => `${BASE_URL}/customers/${id}`
  },

  // --- BRANCH (Şube Yönetimi) ---
  BRANCHES: {
    BASE: `${BASE_URL}/branches`,
    GET_BY_ID: (id: number) => `${BASE_URL}/branches/${id}`
  }
};