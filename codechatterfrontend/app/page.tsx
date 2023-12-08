import Image from 'next/image'
import Swr from 'swr'
import React from 'react';
import useSWR from 'swr';
import { User } from './IUser';

export default function Home() {
  const fetcher = async (url: RequestInfo | URL) => {
    const response = await fetch(url);
  
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
  
    return response.json();
  };
  function ChargingTransactionStatusView() {
  const { data, error } = useSWR<User[]>('http://localhost:3966/frontend/transactions', fetcher);

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (!data) {
    return <div>Loading...</div>;
  }

  return (
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Value</th>
        </tr>
      </thead>
      <tbody>
        {data.map((item) => (
          <tr key={item.id}>
            <td>{item.id}</td>
            <td>{item.name}</td>
            <td>{item.value}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
        }
      }

   