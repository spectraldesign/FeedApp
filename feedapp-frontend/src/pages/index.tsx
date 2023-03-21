import { usePolls } from '@/common/hooks/polls/usePolls'
import { Autocomplete, Flex } from '@mantine/core'
import { useRouter } from 'next/router'

export default function Home() {
  const router = useRouter()
  const { data: polls } = usePolls()
  const formattedPolls: string[] = polls?.map(x => x.id) || []
  return (
    <>
      <Flex mih={50} bg='rgba(0, 0, 0, .3)' gap='md' justify='center' align='center' direction='column' wrap='wrap' h={'100%'}>
        <Autocomplete size='xl' placeholder='Poll ID' data={formattedPolls} onItemSubmit={x => router.push(`/poll/${x.value}`)} />
      </Flex>
    </>
  )
}
