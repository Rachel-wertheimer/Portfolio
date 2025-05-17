import React, { useState } from 'react';
import emailjs from 'emailjs-com';


export const Contact = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');

  const handleSendEmail = () => {
    const templateParams = {
      from_name: name,
      reply_to: email,
      message: message,
      to_email: 'rachel.fsd108@gmail.com',
    };

    emailjs.send(
      process.env.REACT_APP_EMAILJS_SERVICE_ID,
      process.env.REACT_APP_EMAILJS_TEMPLATE_ID,
      templateParams,
      process.env.REACT_APP_EMAILJS_USER_ID
    )
    .then(() => {
      alert('Email sent successfully!');
      setName('');
      setEmail('');
      setMessage('');
    })
    .catch((error) => {
      console.error('Failed to send email:', error);
      alert('Failed to send email. Please try again.');
    });
  };

  return (
    <section id="contact" className="w-full py-20 px-6 bg-[#f0fbfa] text-gray-800 text-center">
      <h2 className="text-3xl md:text-4xl font-bold text-gray-900 mb-2">Contact Me :)</h2>
      <p className="text-sm text-gray-600 mb-6">
         <a href="mailto:rachel.fsd108@gmail.com" className="text-[#00bfa6] underline">rachel.fsd108@gmail.com</a> |  053-3123084
      </p>
      <p className="text-gray-600 mb-8">Have a question or want to work together? Send me a message!</p>

      <div className="flex flex-col items-center space-y-4">
        <input
          type="text"
          placeholder="Your name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="w-72 md:w-96 px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-[#00bfa6]"
        />
        <input
          type="email"
          placeholder="Your email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="w-72 md:w-96 px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-[#00bfa6]"
        />
        <textarea
          rows="5"
          placeholder="Your message..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          className="w-72 md:w-96 px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-[#00bfa6] resize-none"
        />
        <button
          onClick={handleSendEmail}
          className="mt-4 bg-[#00bfa6] text-white px-6 py-2 rounded-md hover:bg-[#00a794] transition-colors"
        >
          Send Email
        </button>
      </div>
    </section>
  );
};
