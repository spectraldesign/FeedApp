export const defaultArgs = {
  retry: 1,
  staleTime: 60 * 1000, //Stale after a minute
}

export const errorMessage = (error?: string) => {
  return {
    id: 'apiError',
    withCloseButton: true,
    autoClose: 5000,
    title: 'An API error has occured',
    message: error || '',
    color: 'red',
  }
}

export const successMessage = (operation?: string) => {
  return {
    id: 'apiSuccess',
    withCloseButton: true,
    autoClose: 5000,
    title: 'API request successful :)',
    message: `${operation} was successful` || '',
    color: 'green',
  }
}
