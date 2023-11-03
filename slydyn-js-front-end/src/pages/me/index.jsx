// @ts-check

import React from 'react'
import MyProfilePage from '@/components/pages/MyProfilePage'
import authServiceInstance from '@/services/Auth.service'


export async function getServerSideProps(context) {
  const me = await authServiceInstance.fetchMeWithSession();

  return {
    props: {
      ...me
    }
  }
}

function MyProfile({
  id,
  userName,
  email,
  firstName,
  lastName
}) {
  return (
    <>
      <h1>{userName}</h1>
    </>
  )
}

export default MyProfile