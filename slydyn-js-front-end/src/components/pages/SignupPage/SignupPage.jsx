// @ts-check
import React from 'react'
import { useSignupForm } from './hooks'

const roleOptions = [
  {
    value: 'Consumer',
    name: 'Consumer'
  },

  {
    value: 'Dealer',
    name: 'Dealer'
  },
  {
    value: 'SuperAdmin',
    name: 'Super Admin'
  },
]


function SignupPage() {
  const { signupForm } = useSignupForm({
    onSignupError: (err) => {
      console.log(err)
    },
    onSignupSuccess: () => {
      console.log('Signup success')
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
        <label htmlFor="email">Email</label>
        <input type="email" id="email" name="email"
          value={signupForm.values.email}
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
      <fieldset>
        <label htmlFor="role">Role</label>
        <select name="role"
          value={signupForm.values.role}
          onChange={signupForm.handleChange}
        >
          {roleOptions.map((roleOption) => (
            <option value={roleOption.value} key={roleOption.value}>{roleOption.name}</option>
          ))}
        </select>
      </fieldset>
      <input type="submit" value="Sign Up" />
      {signupForm.errors && (
        <ul>
          {Object.keys(signupForm.errors).map((error) => (
            <li key={error}>{signupForm.errors[error]}</li>
          ))}
        </ul>
      )
      }
      {signupForm.isSubmitting && (
        <div>Signing up...</div>
      )}
    </form>
  )
}

export default SignupPage