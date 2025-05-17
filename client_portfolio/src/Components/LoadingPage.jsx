import React from 'react';

export const LoadingPage = () => {
  return (
    <div className="h-screen flex flex-col justify-center items-center bg-white">
      <div className="animate-spin rounded-full h-16 w-16 border-4 border-[#00bfa6] border-t-transparent"></div>
      <p className="mt-4 text-lg text-[#00bfa6] font-medium">Loading...</p>
    </div>
  );
};
