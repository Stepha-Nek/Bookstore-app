export interface Book {
  id: number;
  title: string;
  author: string;
  yearPublished: number;
}

export interface AuthResponse {
  token: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  fullName: string;
  email: string;
  password: string;
}