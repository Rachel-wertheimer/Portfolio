import { useEffect, useState } from "react";
import { LoadingPage } from "./LoadingPage";
import { getRepositories, searchRepositoriesByLanguage } from "../Service/api";
import { motion } from "framer-motion";
import { FaGithub } from "react-icons/fa";

export const Projects = () => {
  const [repositories, setRepositories] = useState([]);
  const [language, setLanguage] = useState('');
  const [loading, setLoading] = useState(true);

  const fetchRepositories = async () => {
    setLoading(true);
    try {
      const data = await getRepositories();
      setRepositories(data);
    } catch (error) {
      console.error("שגיאה בשליפת פרויקטים", error);
    } finally {
      setLoading(false);
    }
  };

  const searchRepositories = async (lang) => {
    setLoading(true); 
    try {
      const encodedLang = encodeURIComponent(lang);
      const data = await searchRepositoriesByLanguage(encodedLang);
      setRepositories(data);
    } catch (error) {
      console.error("שגיאה בחיפוש פרויקטים", error);
    } finally {
      setLoading(false); 
    }
  };

  useEffect(() => {
    fetchRepositories();
  }, []);

  useEffect(() => {
    const delayDebounce = setTimeout(() => {
      if (language === '') {
        fetchRepositories();
      } else {
        searchRepositories(language);
      }
    }, 400);

    return () => clearTimeout(delayDebounce);
  }, [language]);

  return (
    <section
      id="projects"
      className="mt-24 text-center mb-8 text-green-700 animate-fade-in"
    >
      <motion.h2
        className="text-3xl md:text-4xl font-bold mb-10 text-gray-900"
        initial={{ opacity: 0, y: 40 }}
        whileInView={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
        viewport={{ once: true }}
      >
        My Projects
      </motion.h2>

      <div className="flex justify-center mb-10">
        <input
          type="text"
          placeholder="Search by language"
          value={language}
          onChange={(e) => setLanguage(e.target.value)}
          className="px-4 py-2 w-64 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-[#00bfa6] text-[#00bfa6]"
        />
      </div>

      {loading ? (
        <LoadingPage />
      ) : (
        <motion.div
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 max-w-6xl mx-auto"
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          transition={{ duration: 0.5 }}
          viewport={{ once: true }}
        >
          {repositories.map((repo) => (
            <motion.div
              key={repo.url}
              className="bg-white rounded-xl shadow-md p-6 transition-all duration-300 hover:shadow-xl hover:-translate-y-2 text-left"
              whileHover={{ scale: 1.02 }}
            >
              <a
                href={repo.url}
                target="_blank"
                rel="noopener noreferrer"
                className="text-xl font-semibold text-[#00bfa6] hover:underline inline-flex items-center gap-2"
              >
                <FaGithub className="text-lg" />
                {repo.name || "No name provided"}
              </a>
              <p className="text-gray-600 mt-2 mb-2 text-sm">
                {repo.description || "No description available"}
              </p>
              <p className="text-gray-700 text-sm">
                <strong>Language:</strong> {repo.language || "Not specified"}
              </p>
              <p className="text-gray-700 text-sm">
                <strong>Presentation:</strong>{" "}
                {repo.presentationUrl ? (
                  <a
                    href={repo.presentationUrl}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-[#00bfa6] underline"
                  >
                    View
                  </a>
                ) : (
                  "Coming soon"
                )}
              </p>
            </motion.div>
          ))}
        </motion.div>
      )}
    </section>
  );
};
