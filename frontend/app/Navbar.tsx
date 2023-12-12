import './globals.css'
import React from "react";
import Link from "next/link";

const Navbar = () => {
  return (
    <div className="bg-base-100 p-4 flex items-center justify-between">
    <div className="flex-1">
      <a className="btn btn-ghost text-xl">My Website</a>
    </div>
    <div className="flex-none">
      <ul className="menu menu-horizontal space-x-2 flex">
        <li>
          <Link href="/MongoTable" className="text-black">Mongo</Link>
        </li>
        <li>
          <Link href="/SQLTable" className="text-black">SQL</Link>
        </li>
      </ul>
    </div>
  </div>
  );
};
export default Navbar;