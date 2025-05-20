import { Inter } from 'next/font/google'
import './globals.css'
import { AuthProvider } from './context/AuthContext';

const inter = Inter({ subsets: ['latin'] })

export const metadata = {
    title: 'Restaurantreservierung',
    description: '',
}

export default function RootLayout({ children }) {
    return (
        <html lang="en">
            <body className={inter.className}>
                <AuthProvider>
                    {children}
                </AuthProvider>
            </body>
        </html>
    )
}