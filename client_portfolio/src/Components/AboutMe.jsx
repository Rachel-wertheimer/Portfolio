import React from 'react';

export const AboutMe = () => {
  return (
    <section
      id="about"
      className="w-full py-20 px-6 bg-[#f0fbfa] text-gray-800 text-center"
    >
      <h2 className="text-4xl md:text-5xl font-bold mb-8 text-gray-900">
        About Me
      </h2>

      <div className="max-w-3xl mx-auto space-y-4 text-lg leading-relaxed">
        <p>
          Hi! I'm a passionate{" "}
          <span className="text-[#00bfa6] font-semibold">Full Stack Developer</span>{" "}
          with practical experience in developing real-world projects.
        </p>
        <p>
          I have extensive knowledge in technologies such as
          <br />
          <span className="text-[#00bfa6] font-medium">
            Python, SQL, Angular, Docker, Node.js, Java, C#, React
          </span>
          , and more.
        </p>
        <p>
          I'm known for my {" "}
          <span className="font-medium">creative thinking</span>, strong{" "}
          <span className="font-medium">self-learning abilities</span>, and{" "}
          <span className="font-medium">effective teamwork skills</span>. I bring a high
          level of proficiency in <span className="text-[#00bfa6] font-semibold">English</span>{" "}.
        </p>
        <p>
          Currently in the process of gaining professional experience at{" "}
          <span className="text-[#00bfa6] font-semibold">HITEL</span>.
        </p>
      </div>
    </section>
  );
};
