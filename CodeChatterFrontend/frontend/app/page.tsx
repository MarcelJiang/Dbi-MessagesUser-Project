import Image from 'next/image'

import  useSWR  from 'swr';
import Navbar from './Navbar';
import MessageTable from './Mongo';


export default function Home() {
  
  return(
    <MessageTable></MessageTable>
  )
    
}

