// @ts-check

import React from 'react'
import MyProfilePage from '@/components/pages/MyProfilePage'
import authServiceInstance from '@/services/Auth.service'


export async function getServerSideProps() {

}

function MyProfile() {
  return (
    <MyProfilePage />
  )
}

export default MyProfile