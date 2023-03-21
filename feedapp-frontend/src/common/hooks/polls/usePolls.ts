import { notifications } from '@mantine/notifications'
import { useQuery } from '@tanstack/react-query'
import { AxiosError } from 'axios'
import { defaultArgs, errorMessage, successMessage } from '..'
import { getAllPolls } from '../../API/poll'
import { Poll } from '../../interfaces/poll'
const getUsePollsKey = () => ['UsePolls']

export const usePolls = () => {
  return useQuery<Poll[], AxiosError>(getUsePollsKey(), () => getAllPolls(), {
    ...defaultArgs,
    onError: (err: AxiosError) => {
      notifications.show(errorMessage(err.cause?.message))
    },
    onSuccess: () => {
      notifications.show(successMessage())
    },
  })
}
