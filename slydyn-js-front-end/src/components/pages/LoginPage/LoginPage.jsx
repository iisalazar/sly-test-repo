// @ts-check
import React from 'react'
import { useLoginForm } from './hooks'
import { useAuthProvider } from '@/context/AuthContext'



function LoginPage() {
  const authProvider = useAuthProvider();
  const { signupForm } = useLoginForm({
    onLoginError: (err) => {
      console.log(err)
    },
    onLoginSuccess: (data) => {
      authProvider.setAccessToken(data.accessToken);
      authProvider.setExpiresIn(data.expiresIn);
    }
  })

  return (
    <form onSubmit={signupForm.handleSubmit}>
      <fieldset>
        <label htmlFor="username">Username</label>
        <input type="text" id="username" name="username"
          value={signupForm.values.username}
          onChange={signupForm.handleChange}
        />
      </fieldset>
      <fieldset>
        <label htmlFor="password">Password</label>
        <input type="password" id="password" name="password"
          value={signupForm.values.password}
          onChange={signupForm.handleChange}
        />
      </fieldset>
      <input type="submit" value="Log in" />
      {signupForm.errors && (
        <ul>
          {Object.keys(signupForm.errors).map((error) => (
            <li key={error}>{signupForm.errors[error]}</li>
          ))}
        </ul>
      )
      }
      {signupForm.isSubmitting && (
        <div>Logging in...</div>
      )}
    </form>
  )
}

export default LoginPage