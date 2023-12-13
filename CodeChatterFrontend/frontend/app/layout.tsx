
import { Inter } from 'next/font/google'
import './globals.css'
import Navbar from './Navbar'





export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
 
    <html lang="en">
      <body>
        <Navbar />
        <main>{children}</main>
      </body>
    </html>
  )
}
