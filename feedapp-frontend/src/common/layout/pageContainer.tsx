import { AppShell, Burger, Button, Footer, Header, MediaQuery, Modal, useMantineTheme } from '@mantine/core'
import { IconExternalLink } from '@tabler/icons-react'
import { useRouter } from 'next/router'
import { useState } from 'react'

interface PageContainerProps {
  children: React.ReactNode | React.ReactNode[]
}

const PageContainer = ({ children }: PageContainerProps) => {
  const theme = useMantineTheme()
  const [opened, setOpened] = useState(false)
  const router = useRouter()
  return (
    <>
      <AppShell
        header={
          <Header height={{ base: 50, md: 70 }} p='md'>
            <div style={{ display: 'flex', alignItems: 'center', height: '100%' }}>
              <MediaQuery largerThan='sm' styles={{ display: 'none' }}>
                <Burger opened={opened} onClick={() => setOpened(o => !o)} size='sm' color={theme.colors.gray[6]} mr='xl'></Burger>
              </MediaQuery>
              <Modal
                transitionProps={{ transition: 'fade', duration: 200, timingFunction: 'linear' }}
                scrollAreaComponent={Modal.NativeScrollArea}
                withCloseButton={false}
                opened={opened}
                onClose={() => setOpened(o => !o)}
              >
                <Button
                  fullWidth
                  variant={'outline'}
                  onClick={() => {
                    setOpened(o => !o)
                    router.push('/')
                  }}
                >
                  HOME
                </Button>
              </Modal>
              <MediaQuery smallerThan='sm' styles={{ display: 'none' }}>
                <Button variant={'outline'} onClick={() => router.push('/')}>
                  FeedApp
                </Button>
              </MediaQuery>
            </div>
          </Header>
        }
        styles={{ main: { paddingBottom: '0px' } }}
        py='0'
      >
        <main style={{ height: '100vh' }}>{children}</main>
      </AppShell>
      <Footer fixed={false} height={60} p='md'>
        <Button
          component='a'
          href='https://feedapplication.azurewebsites.net/swagger/index.html'
          target={'_blank'}
          rightIcon={<IconExternalLink size='0.9rem' />}
          variant={'outline'}
          onClick={() => router.push('/')}
        >
          API
        </Button>
      </Footer>
    </>
  )
}

export default PageContainer
