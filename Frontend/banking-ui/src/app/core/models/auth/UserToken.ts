// TOKEN İÇERİĞİ (Decoded JWT)

export interface UserToken {
    name: string;
    role: string[];
    sub: string; // UserId
    exp: number; // Süre bitişi
}
