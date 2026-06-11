import React from "react";
import Header from "../components/layout/Header";
import Footer from "../components/layout/Footer";
import studentSupportImage from "../assets/student-support.jpg"; 
function Home() {
  return (
    <div className="bg-gray-100 min-h-screen flex flex-col">
      <main className="flex-1 w-full p-8 text-center">
        <section className="mb-12">
          <h1 className="text-4xl font-bold mb-4">Share Knowledge.</h1>
          <p className="mb-6">
            Connect with fellow Fontys students, share your semester experiences,
            ask questions, and build a stronger academic community together.
          </p>
          <button className="bg-[#9c78a1] text-white py-2 px-4 rounded font-bold">
            Join The Community →
          </button>
        </section>

        <section className="mb-12 flex justify-center">
         <img
  src={studentSupportImage}
  alt="Fontys student support community"
  className="w-full max-w-full rounded-lg shadow-lg"
/>

        </section>

        <section className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 text-left">
          <div className="bg-gray-800 p-4 rounded text-white">
            <h3 className="font-bold mb-2">Share Experiences</h3>
            <p>Post about your semester journey, tools used, challenges faced, and solutions discovered.</p>
          </div>
          <div className="bg-gray-800 p-4 rounded text-white">
            <h3 className="font-bold mb-2">Q&A Community</h3>
            <p>Ask questions, get answers from experienced students, and help others learn.</p>
          </div>
          <div className="bg-gray-800 p-4 rounded text-white">
            <h3 className="font-bold mb-2">Points & Levels</h3>
            <p>Earn recognition for contributions and climb the leaderboard as you help others.</p>
          </div>
          <div className="bg-gray-800 p-4 rounded text-white">
            <h3 className="font-bold mb-2">Smart Search</h3>
            <p>Find relevant posts and answers quickly with advanced filtering by semester and program.</p>
          </div>
        </section>
      </main>
    </div>
  );
}

export default Home;

