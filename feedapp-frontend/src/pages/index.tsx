import { usePolls } from '@/common/hooks/polls/usePolls'
import { Autocomplete, Button, Flex, Loader } from '@mantine/core'
import { useRouter } from 'next/router'
import { useRef, useState } from 'react'

export default function Home() {
  const timeoutRef = useRef<number>(-1)
  const [data, setData] = useState<string[]>([])
  const [value, setValue] = useState('')
  const [loading, setLoading] = useState(false)
  const router = useRouter()
  const { data: polls } = usePolls()

  const handleChange = (val: string) => {
    window.clearTimeout(timeoutRef.current)
    setValue(val)
    setData([])

    if (val.trim().length === 0 || val.includes('@')) {
      setLoading(false)
    } else {
      setLoading(true)
      timeoutRef.current = window.setTimeout(() => {
        setLoading(false)
        setData(polls?.map(x => x.id) || [])
      }, 1000);
    }
  }

  return (
    <>
      <Flex mih={50} bg='rgba(0, 0, 0, .3)' gap='md' justify='center' align='center' direction='column' wrap='wrap' h={'100%'}>
        <Autocomplete
          value={value}
          size='xl'
          placeholder='Poll ID'
          onChange={handleChange}
          rightSection={loading ? <Loader size={16} /> : null}
          data={data}
          onItemSubmit={x => router.push(`/poll/${x.value}`)}
        />
      </Flex>
    </>
  )
}
