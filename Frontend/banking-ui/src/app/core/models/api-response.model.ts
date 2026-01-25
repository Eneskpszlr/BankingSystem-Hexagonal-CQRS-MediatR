export interface CommandResponse {
    success: boolean;
    message: string;
    entityId?: number;       // Sadece Create işleminde döner
    referenceNumber?: string; // Sadece Transfer işleminde döner
}

export interface ErrorResponse {
    success: boolean;
    message: string;         // "Validasyon hatası"
    exceptionType?: string;  // "ValidationException"
    errors?: string[];       // ["Email boş olamaz"]
}