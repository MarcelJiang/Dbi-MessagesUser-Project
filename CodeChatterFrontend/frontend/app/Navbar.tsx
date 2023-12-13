import Link from 'next/link';

const Navbar = () => {
  return (
    <nav className="bg-gray-800 p-4">
      <div className="container mx-auto flex justify-between items-center">
        <Link href="/">
          <div className="text-white text-xl font-bold cursor-pointer">CodeChatter</div>
        </Link>

        <div className="space-x-4 flex items-center">
          <Link href="/">
            <div className="text-white hover:text-gray-300 cursor-pointer">Mongo</div>
          </Link>
          <Link href="/sql">
            <div className="text-white hover:text-gray-300 cursor-pointer">SQL</div>
          </Link>
          
        </div>
      </div>
    </nav>
  );
};

export default Navbar;