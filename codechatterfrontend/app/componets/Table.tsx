import useSWR from 'swr'
import { User } from '../IUser';
const fetcher = async (url: RequestInfo | URL) => {
    const response = await fetch(url);
  
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
  
    return response.json();
  };
  function ChargingTransactionStatusView() {
  const { data, error } = useSWR<User[]>('https://localhost:54614/api/v1/users', fetcher);

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
          <tr key={item.guid}>
            <td>{item.guid}</td>
            <td>{item.emailAddress}</td>
            <td>{item.username}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
        }

export default ChargingTransactionStatusView;