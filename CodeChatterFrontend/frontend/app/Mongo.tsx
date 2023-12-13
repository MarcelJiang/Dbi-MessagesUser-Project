"use client"
import Image from 'next/image'

import  useSWR  from 'swr';
import Navbar from './Navbar';

interface Message {
  id: string;
  content: string;
  dateAndTime: string;
  textChannelId: string;
  userId: string;
  chatroomId: string;
}

interface UserWithMessages {
  userId: string;
  userName: string;
  messages: Message[];
}

const fetcher = async (url: string) => {
  const response = await fetch(url);

  if (!response.ok) {
    throw new Error('Failed to fetch data');
  }
  return response.json();
}  

  const MessageTable: React.FC = () => {
    const { data, error } = useSWR<UserWithMessages[]>("https://localhost:54614/Api/v1/Messages/all-with-messages?api-version=1",fetcher);
  
    if (error) {
      return <div>Error: No Data</div>;
    }
  
    if (!data) {
      return <div>Loading...</div>;
    }
  
    return (
        <div className="grid gap-4">
  {data.map((user) => (
    <div key={user.userId} className="bg-white p-4 rounded-lg shadow-md">
      <h2 className="text-xl font-bold mb-2">{user.userName}</h2>
      <div className="overflow-x-auto">
        <div className="flex flex-col min-w-full">
          <div className="grid grid-cols-6 bg-gray-100 p-2 font-semibold">
            <div>ID</div>
            <div>Content</div>
            <div>Date and Time</div>
           
          </div>
          {user.messages.map((message) => (
            <div key={message.id} className="grid grid-cols-6 border-t p-2">
              <div>{message.id}</div>
              <div>{message.content}</div>
              <div>{new Date(message.dateAndTime).toLocaleString()}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  ))}
</div>
  );
    
}
export default MessageTable;
