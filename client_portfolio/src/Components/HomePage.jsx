import React from 'react';

import 'bootstrap/dist/css/bootstrap.min.css';
import { AboutMe } from './AboutMe';
import { Projects } from './Projects ';
import { Contact } from './Contact ';
import { HeroSection } from './HeroSection';



export const HomePage = () => {
  return (
    <div className="flex flex-col items-center">
      <HeroSection></HeroSection>
      <AboutMe></AboutMe>
      <Projects></Projects>
      <Contact></Contact>
    </div>
  );
}
