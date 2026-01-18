export interface ApiResponse<T> {
    success: boolean;
    message: string;
    errors?: string[];     // Backend'de ValidationException dönerse burası dolar
    data: T;               // Asıl veri burada
    entityId?: number;     // Create işleminden sonra dönen ID
}