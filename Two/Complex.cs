﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal
{
    struct Complex
    {
        public double real;
        public double imaginary;

        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
        }
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real*c2.real -c1.imaginary*c2.imaginary, c1.imaginary*c2.real+c1.real*c2.imaginary);
        }

        public double sqre()
        {
            return this.real * this.real + this.imaginary * this.imaginary;
        }
    }
}
