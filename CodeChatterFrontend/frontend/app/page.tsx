import Image from 'next/image'

import useSWR from 'swr';
import Navbar from './Navbar';

interface ReadMessageDto {
  Id: string;
  Content: string;
  DateAndTime: Date;
  TextChannelId: string;
  UserId: string;
  ChatroomId: string;
}

interface Props {
  apiUrl: string; // The URL to fetch the data from
}
export default function Home() {
  // const MessageTable: React.FC<Props> = ({ apiUrl }) => {
  //   const { data, error } = useSWR<ReadMessageDto[]>(apiUrl);
  
  //   if (error) {
  //     return <div>Error: {error.message}</div>;
  //   }
  
  //   if (!data) {
  //     return <div>Loading...</div>;
  //   }
  
    return (
      <div>
      
      <div>
      <table className="min-w-full bg-white border border-gray-300">
        <thead>
          <tr>
            <th className="py-2 px-4 border-b">ID</th>
            <th className="py-2 px-4 border-b">Content</th>
            <th className="py-2 px-4 border-b">Date and Time</th>
            <th className="py-2 px-4 border-b">Text Channel ID</th>
            <th className="py-2 px-4 border-b">User ID</th>
            <th className="py-2 px-4 border-b">Chatroom ID</th>
          </tr>
        </thead>
       {/*  <tbody>
          {data.map((message) => (
            <tr key={message.Id}>
              <td className="py-2 px-4 border-b">{message.Id}</td>
              <td className="py-2 px-4 border-b">{message.Content}</td>
              <td className="py-2 px-4 border-b">{message.DateAndTime.toLocaleString()}</td>
              <td className="py-2 px-4 border-b">{message.TextChannelId}</td>
              <td className="py-2 px-4 border-b">{message.UserId}</td>
              <td className="py-2 px-4 border-b">{message.ChatroomId}</td>
            </tr>
          ))}
        </tbody> */}
      </table>
      </div>
      </div>
    );
}

