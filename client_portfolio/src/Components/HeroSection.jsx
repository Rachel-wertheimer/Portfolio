
import { motion } from "framer-motion";
import { FaReact, FaNodeJs, FaDocker, FaGithub, FaEnvelope, FaJava, FaPython, FaDatabase, FaGit, FaAws } from "react-icons/fa"; // הוספתי אייקון לדואר אלקטרוני
import { DiMongodb } from "react-icons/di";

export const HeroSection = () => {
  return (
    <section className="w-full min-h-screen bg-gradient-to-b from-white via-[#e0f7f5] to-[#00bfa6] flex flex-col justify-center items-center px-4 text-center relative">

      <div className="absolute top-20 left-1/2 transform -translate-x-1/2 w-64 h-64 bg-[#b2f5ea] opacity-30 rounded-full blur-3xl z-0"></div>

      <div className="relative z-10">
        <img
          src="Logo.jpg"
          alt="Rachel Wertheimer"
          className="w-44 h-44 rounded-full shadow-2xl mb-6 object-cover border-4 border-white"
        />
      </div>

      <h1 className="text-4xl md:text-5xl font-extrabold text-gray-900 z-10">
        Rachel Wertheimer
      </h1>

      <p className="text-lg md:text-xl text-gray-700 max-w-xl mt-2 mb-6 z-10">
        Hi, I'm Rachel Wertheimer – a Full Stack Developer with hands-on experience building real-world projects.
      </p>

      <p className="text-base text-gray-600 max-w-xl mb-8 z-10">
        I'm passionate about development and love creating clean, smart solutions with modern technologies.
      </p>

      <p className="text-base text-gray-600 max-w-xl mb-8 z-10">
        Feel free to explore my projects or check out my resume.
      </p>

      <motion.div
        className="flex flex-col sm:flex-row gap-4 z-10"
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ delay: 0.3, duration: 0.8 }}
      >
        <a
          href="#projects"
          className="px-6 py-3 bg-[#00bfa6] hover:bg-[#009e8a] text-white rounded-full text-base font-semibold shadow transition duration-300 flex items-center justify-center gap-2"
        >
          <FaGithub />
          View My Projects
        </a>
        <a
          href="./קורות חיים רחל ורטהיימר.docx"
          target="_blank"
          className="px-6 py-3 border border-[#00bfa6] text-[#00bfa6] hover:bg-[#00bfa6] hover:text-white rounded-full text-base font-semibold transition duration-300 flex items-center justify-center gap-2"
        >
          <FaEnvelope />
          View Resume
        </a>
      </motion.div>

      <div className="flex gap-6 text-3xl text-[#00bfa6] mt-12 z-10">
        <FaReact title="React" />
        <FaNodeJs title="Node.js" />
        <FaDocker title="Docker" />
        <FaGithub title="GitHub" />
        <FaJava title="Java" />
        <FaPython title="Python" />
        <FaDatabase title="Database" />
        <FaGit title="Git" />
        <FaAws title="AWS" />
        <DiMongodb title="MongoDB" />
      </div>
    </section>
  );
};
