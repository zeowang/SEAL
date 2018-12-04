﻿using Microsoft.Research.SEAL.Tools;
using System;

namespace Microsoft.Research.SEAL
{
    /// <summary>Stores a set of attributes (qualifiers) of a set of encryption parameters.</summary>
    /// 
    /// <remarks>
    /// Stores a set of attributes (qualifiers) of a set of encryption parameters. These 
    /// parameters are mainly used internally in various parts of the library, e.g. to 
    /// determine which algorithmic optimizations the current support. The qualifiers are 
    /// automatically created by the <see cref="SEALContext" /> class, silently passed 
    /// on to classes such as <see cref="Encryptor" />, <see cref="Evaluator" />, and 
    /// <see cref="Decryptor" />, and the only way to change them is by changing the 
    /// encryption parameters themselves. In other words, a user will never have to create 
    /// their own instance of EncryptionParameterQualifiers, and in most cases never have 
    /// to worry about them at all.
    /// </remarks>
    /// <seealso>See EncryptionParameters.GetQualifiers() for obtaining the 
    /// EncryptionParameterQualifiers corresponding to a certain parameter set.</seealso>
    public class EncryptionParameterQualifiers : NativeObject
    {
        public EncryptionParameterQualifiers(EncryptionParameterQualifiers copy)
        {
            if (null == copy)
                throw new ArgumentNullException(nameof(copy));

            IntPtr ptr;
            NativeMethods.EPQ_Create(copy.NativePtr, out ptr);
            NativePtr = ptr;
        }

        /// <summary>
        /// Create an instance of Encryption Parameter Qualifiers through a pointer to
        /// a native object.
        /// </summary>
        /// <param name="ptr">Pointer to native Encryption Parameter Qualifiers.</param>
        /// <param name="owned">Whether this instance owns the native pointer.</param>
        internal EncryptionParameterQualifiers(IntPtr ptr, bool owned = true)
            : base(ptr, owned)
        {
        }

        /// <summary>
        /// If the encryption parameters are set in a way that is considered valid by SEAL,
        /// the variable ParametersSet is set to true.
        /// </summary>
        public bool ParametersSet
        {
            get
            {
                bool result;
                NativeMethods.EPQ_ParametersSet(NativePtr, out result);
                return result;
            }
        }

        /// <summary>
        /// Tells whether FFT can be used for polynomial multiplication.
        /// </summary>
        /// <remarks>
        /// Tells whether FFT can be used for polynomial multiplication. If the polynomial modulus 
        /// is of the form X^N+1, where N is a power of two, then FFT can be used for fast 
        /// multiplication of polynomials modulo the polynomial modulus. In this case the variable 
        /// EnableFFT will be set to true. However, currently SEAL requires the polynomial modulus
        /// to be of this form for the parameters to be valid. Therefore, ParametersSet can only 
        /// be true if EnableFFT is true.
        /// </remarks>
        public bool EnableFFT
        {
            get
            {
                bool result;
                NativeMethods.EPQ_EnableFFT(NativePtr, out result);
                return result;
            }
        }

        /// <summary>
        /// Tells whether NTT can be used for polynomial multiplication.
        /// </summary>
        /// <remarks>
        /// Tells whether NTT can be used for polynomial multiplication. If the primes in the
        /// coefficient modulus are congruent to 1 modulo 2N, where X^N+1 is the polynomial
        /// modulus and N is a power of two, then the number-theoretic transform (NTT) can be
        /// used for fast multiplications of polynomials modulo the polynomial modulus and
        /// coefficient modulus. In this case the variable EnableNTT will be set to true. However,
        /// currently SEAL requires this to be the case for the parameters to be valid. Therefore,
        /// ParametersSet can only be true if EnableNTT is true.
        /// </remarks>
        public bool EnableNTT
        {
            get
            {
                bool result;
                NativeMethods.EPQ_EnableNTT(NativePtr, out result);
                return result;
            }
        }

        /// <summary>
        /// Tells whether batching is supported by the encryption parameters.
        /// </summary>
        /// <remarks>
        /// Tells whether batching is supported by the encryption parameters. If the plaintext
        /// modulus is congruent to 1 modulo 2N, where X^N+1 is the polynomial modulus and N is
        /// a power of two, then it is possible to use the PolyCRTBuilder class to view plaintext
        /// elements as 2-by-(N/2) matrices of integers modulo the plaintext modulus. This is
        /// called batching, and allows the user to operate on the matrix elements (slots) in
        /// a SIMD fashion, and rotate the matrix rows and columns. When the computation is
        /// easily vectorizable, using batching can yield a huge performance boost. If the
        /// encryption parameters support batching, the variable EnableBatching is set to true.
        /// </remarks>
        public bool EnableBatching
        {
            get
            {
                bool result;
                NativeMethods.EPQ_EnableBatching(NativePtr, out result);
                return result;
            }
        }

        ///
        /// <summary>
        /// Tells whether fast plain lift is supported by the encryption parameters.
        /// </summary>
        /// <remarks>
        /// Tells whether fast plain lift is supported by the encryption parameters. A certain
        /// performance optimization in multiplication of a ciphertext by a plaintext
        /// (Evaluator::MultiplyPlain) and in transforming a plaintext element to NTT domain
        /// (Evaluator::TransformToNTT) can be used when the plaintext modulus is smaller than
        /// each prime in the coefficient modulus. In this case the variable EnableFastPlainLift
        /// is set to true.
        /// </remarks>
        public bool EnableFastPlainLift
        {
            get
            {
                bool result;
                NativeMethods.EPQ_EnableFastPlainLift(NativePtr, out result);
                return result;
            }
        }

        /// <summary>
        /// Destroy native object.
        /// </summary>
        protected override void DestroyNativeObject()
        {
            NativeMethods.EPQ_Destroy(NativePtr);
        }
    }
}