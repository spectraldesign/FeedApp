import { AppShell, Burger, Footer, Header, MediaQuery, Text, useMantineTheme } from '@mantine/core'
import { useState } from 'react'

interface PageContainerProps {
  children: React.ReactNode | React.ReactNode[]
}

const PageContainer = ({ children }: PageContainerProps) => {
  const theme = useMantineTheme()
  const [opened, setOpened] = useState(false)
  return (
    <>
      <AppShell
        header={
          <Header height={{ base: 50, md: 70 }} p='md'>
            <div style={{ display: 'flex', alignItems: 'center', height: '100%' }}>
              <MediaQuery largerThan='sm' styles={{ display: 'none' }}>
                <Burger opened={opened} onClick={() => setOpened(o => !o)} size='sm' color={theme.colors.gray[6]} mr='xl' />
              </MediaQuery>
              <Text>Application header</Text>
            </div>
          </Header>
        }
        styles={{ main: { paddingBottom: '0px' } }}
        py='0'
      >
        <main style={{ height: '100vh' }}>{children}</main>
      </AppShell>
      <Footer fixed={false} height={60} p='md'>
        Application footer
      </Footer>
    </>
  )
}

export default PageContainer
